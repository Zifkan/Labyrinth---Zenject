using System;
using System.Linq;
using Assets.Scripts.Character.Inventory;
using Assets.Scripts.InteractiveObjects;
using Assets.Scripts.InteractiveObjects.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Character
{
    public abstract class GameCharacter : MonoBehaviour, IGameCharacter
    {
        private const float ShiftHeight = 0.1f;

        private CellOption[,] _cellOptions;
        
        private Animator _animator;
        private int CellX { get; set; }
        private int CellY { get; set; }

        private bool _isGrab;
        private int _kX;
        private int _kY;
        private GrabObject _grabObject;
        private int _oldParentX;
        private int _oldParentY;

        private bool _isMove;
        private bool _isIdle;

        private float _moveTime = 1.0f;
        private float _moveTimer;
        private Vector3 _forward;
        private float _vSpeed;

        private int _playerDirectionX;
        private int _playerDirectionY;
        private CharacterDirection _characterDirection;
        

        [Inject]
        private GameInventory Inventory { get; set; }

        private CellOption CurrentCell 
        {
            get { return _cellOptions[CellX, CellY]; }
        }

        private bool IsIdle
        {
            set
            {
                _isIdle = value;
                _animator.SetBool("Idle", _isIdle);
            }
        }

        private CharacterDirection CharDirection
        {
            get { return _characterDirection; }
            set
            {
                _characterDirection = value;
                if (value < 0)
                    _characterDirection=CharacterDirection.Left;
                if (value > (CharacterDirection) 3)
                    _characterDirection = CharacterDirection.Up;
                switch (_characterDirection)
                {
                    case CharacterDirection.Up:
                        _playerDirectionX = 0;
                        _playerDirectionY = 1;
                        break;
                    case CharacterDirection.Down:
                        _playerDirectionX = 0;
                        _playerDirectionY = -1;
                        break;
                    case CharacterDirection.Right:
                        _playerDirectionX = 1;
                        _playerDirectionY = 0;
                        break;
                    case CharacterDirection.Left:
                        _playerDirectionX = -1;
                        _playerDirectionY = 0;
                        break;
                }
            }
        }

        protected abstract void CheckInput();

        public void Initialize(CellOption[,] cellOptions, int startX, int startY,CharacterDirection direction)
        {
            _cellOptions = cellOptions;
            CellX = startX;
            CellY = startY;
            
            transform.position = _cellOptions[startX, startY].transform.position;

            _animator = GetComponent<Animator>();
            _animator.SetFloat("VSpeed", 0);
            IsIdle = true;
            CharDirection = direction;
        }

        private void Update()
        {
            CheckInput();

            if (_isMove)
            {
                IsIdle = false;
                _animator.SetFloat("VSpeed", _vSpeed);

                _moveTimer += Time.deltaTime;

                if (_isGrab)
                {
                    _grabObject.transform.position = Vector3Lerp(_grabObject.transform.position, _cellOptions[CellX + _kX, CellY + _kY].transform.position, (_moveTimer /( _moveTime * 15)));
                }


                if (_moveTimer >= _moveTime)
                {
                    _isMove = false;
                    _moveTimer = 0;
                    IsIdle = true;
                    _animator.SetFloat("VSpeed", 0);
                    transform.position = _forward;
                    
                    MoveGrabedObject();
                }
            }
        }

        protected void MoveForward()
        {
            Move(1);
        }

        protected void MoveBackward()
        {
            Move(-1);
        }

        protected void MoveRight()
        {
            if (_isMove || _isGrab) return;

            if (CharDirection == CharacterDirection.Down)
                CharDirection = CharacterDirection.Right;
            else
                CharDirection++;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Quaternion.AngleAxis(90 * GetRotation(), Vector3.up).eulerAngles);
        }

        protected void MoveLeft()
        {
            if (_isMove || _isGrab) return;

            if (CharDirection == CharacterDirection.Down)
                CharDirection = CharacterDirection.Left;
            else
                CharDirection --;

            transform.rotation =Quaternion.Euler(transform.rotation.eulerAngles +Quaternion.AngleAxis(-90*GetRotation(), Vector3.up).eulerAngles);
        }

        protected void UseObject()
        {
            var obj = _cellOptions[CurrentCell.X + _playerDirectionX, CurrentCell.Y + _playerDirectionY].LevelObject;

            if (obj == null || !obj.IsUsable) return;

            var usableObject = obj.GetComponent<IUsableObject>();
            usableObject.UseObject(this);
        }

        private void Movement(CellOption nextCell)
        {
            _forward = nextCell.transform.position + new Vector3(0, ShiftHeight, 0);
            
            CellX = nextCell.X;
            CellY = nextCell.Y;
            _isMove = true;
        }

        private void MoveGrabedObject()
        {
            if (!_isGrab) return;
            _cellOptions[_oldParentX, _oldParentY].LevelObject = null;
            _grabObject.transform.position = _cellOptions[CellX + _kX, CellY + _kY].transform.position;
            _oldParentX = _grabObject.ParentX = CellX + _kX;
            _oldParentY = _grabObject.ParentY = CellY + _kY;
            _cellOptions[_grabObject.ParentX, _grabObject.ParentY].LevelObject = _grabObject;
        }

        public void AddInventoryItem(int linkedItemX, int linkedItemY, string itemName)
        {
            Inventory.AddItem(linkedItemX, linkedItemY, itemName);
        }

        public void GrabObject(GrabObject grabObject)
        {
            _isGrab = !_isGrab;
            _kX = grabObject.ParentX - CurrentCell.X;
            _kY = grabObject.ParentY - CurrentCell.Y;
            _grabObject = grabObject;
            _oldParentX = _grabObject.ParentX;
            _oldParentY = _grabObject.ParentY;
        }

        public void UseKeyDoor(KeyDoor keyDoor)
        {
            var inventoryItem = Inventory.Items.FirstOrDefault(item => item.LinkedItemX == keyDoor.ParentX && item.LinkedItemY == keyDoor.ParentY);
            if (inventoryItem == null) return;

            keyDoor.OpenDoor();
            Inventory.RemoveItem(inventoryItem);
        }

        private Vector3 Vector3Lerp(Vector3 from, Vector3 to, float t)
        {
            return new Vector3(
                Mathf.Lerp(from.x,to.x,t),
                Mathf.Lerp(from.y, to.y, t),
                Mathf.Lerp(from.z,to.z,t));
        }

        private int GetRotation()
        {
            return transform.rotation.eulerAngles.y >= 180 && transform.rotation.eulerAngles.y <= 181 ? -1 : 1;
        }

        private void Move(int direction)
        {
            if (_isMove) return;
            var nextCell = _cellOptions[CellX + _playerDirectionX * direction, CellY + _playerDirectionY * direction];



            if (_isGrab && direction>0)
            {
                var grabNextCell = _cellOptions[CellX + _playerDirectionX * 2 * direction, CellY + _playerDirectionY * 2 * direction];
                if (grabNextCell.LevelObject != null && !grabNextCell.LevelObject.CanMove) return;
            }
            else
            {
                if (nextCell.LevelObject != null && !nextCell.LevelObject.CanMove) return;
            }


            _vSpeed = direction;
            Movement(nextCell);
        }
    }


    public enum CharacterDirection
    {
        Up,
        Right,
        Down,
        Left
    }
}
