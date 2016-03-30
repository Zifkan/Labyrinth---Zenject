using System.Collections.Generic;
using Assets.Scripts.Serialization;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class CellGrid 
    {
        private readonly CellOption _cellPrefab;
        private const float CellShift = 0.05f;
        private List<CellOption> _cellOptions ;

        public CellGrid()
        {
            _cellPrefab = GameResourses.Instance.GetCellPrefab();
        }

        public List<CellOption> OnCreateCells(GameObject cellsParent, int cellsRow, int cellsColumn, List<CellConfig> cellConfigs)
        {
            int i = -1;
            _cellOptions = new List<CellOption>();
            for (int row = 0; row < cellsRow; row++)
            {
                for (int column = 0; column < cellsColumn; column++)
                {
                    i++;
                    var cell = CreateCell(row, column);
                    
                    cell.transform.parent = cellsParent.transform;
                    if (cellConfigs != null)
                    {
                        var cellConf = cellConfigs[i];
                        if (cellConf != null)
                        {
                            cell.LinkedItemX = cellConf.LinkedItemColumn;
                            cell.LinkedItemY = cellConf.LinkedItemRow;
                        }
                    }
                    _cellOptions.Add(cell);
                }
            }
            return _cellOptions;
        }

        private CellOption CreateCell(int row, int column)
        {
            var cell = Object.Instantiate(_cellPrefab);
            cell.gameObject.SetActive(true);
            cell.transform.position = new Vector3(column + column * CellShift, 0, row + row * CellShift);
            cell.X = column;
            cell.Y = row;
            return cell;
        }
    }
}
