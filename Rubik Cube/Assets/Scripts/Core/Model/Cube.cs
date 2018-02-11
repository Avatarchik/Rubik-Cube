﻿using System.Linq;

namespace Assets.Scripts.Core.Model
{
    /// <summary>
    /// 
    /// Y
    /// ^
    /// |  ------- ------- ------- 
    /// | | 0,2,Z | 1,2,Z | 2,2,Z |
    /// |  ------- ------- ------- 
    /// | | 0,1,Z | 1,1,Z | 2,1,Z |
    /// |  ------- ------- ------- 
    /// | | 0,0,Z | 1,0,Z | 2,0,Z |
    /// |  ------- ------- ------- 
    /// |---------------- > X
    /// 
    /// Z - layer's height
    ///
    /// </summary>
    public sealed class Cube
    {
        private readonly Piece[,,] _pieces = new Piece[3, 3, 3];

        public Piece this[int x, int y, int z] => _pieces[x, y, z];

        public Cube()
        {
            InitSolvedCube();
        }

        private void InitSolvedCube()
        {
            #region BOTTOM LAYER

            // First row
            _pieces[0, 0, 0] = new CornerPiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.DOWN), new Sticker(Faces.LEFT) });
            _pieces[1, 0, 0] = new EdgePiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.DOWN) });
            _pieces[2, 0, 0] = new CornerPiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.DOWN), new Sticker(Faces.RIGHT)});

            // Second row
            _pieces[0, 1, 0] = new EdgePiece(new[] { new Sticker(Faces.LEFT), new Sticker(Faces.DOWN) });
            _pieces[1, 1, 0] = new CentralPiece(new[] { new Sticker(Faces.DOWN) });
            _pieces[2, 1, 0] = new EdgePiece(new[] { new Sticker(Faces.RIGHT), new Sticker(Faces.DOWN) });

            // Third row
            _pieces[0, 2, 0] = new CornerPiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.DOWN), new Sticker(Faces.LEFT) });
            _pieces[1, 2, 0] = new EdgePiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.DOWN) });
            _pieces[2, 2, 0] = new CornerPiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.DOWN), new Sticker(Faces.RIGHT) });

            #endregion BOTTOM LAYER

            #region MIDDLE LAYER

            // First row
            _pieces[0, 0, 1] = new EdgePiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.LEFT) });
            _pieces[1, 0, 1] = new CentralPiece(new[] { new Sticker(Faces.FRONT) });
            _pieces[2, 0, 1] = new EdgePiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.RIGHT) });

            // Second row
            _pieces[0, 1, 1] = new CentralPiece(new[] { new Sticker(Faces.LEFT) });
            _pieces[1, 1, 1] = new EmptyPiece();
            _pieces[2, 1, 1] = new CentralPiece(new[] { new Sticker(Faces.RIGHT) });

            // Third row
            _pieces[0, 2, 1] = new EdgePiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.LEFT) });
            _pieces[1, 2, 1] = new CentralPiece(new[] { new Sticker(Faces.BACK) });
            _pieces[2, 2, 1] = new EdgePiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.RIGHT) });

            #endregion MIDDLE LAYER

            #region TOP LAYER

            // First row
            _pieces[0, 0, 2] = new CornerPiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.UP), new Sticker(Faces.LEFT) });
            _pieces[1, 0, 2] = new EdgePiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.UP) });
            _pieces[2, 0, 2] = new CornerPiece(new[] { new Sticker(Faces.FRONT), new Sticker(Faces.UP), new Sticker(Faces.RIGHT) });

            // Second row
            _pieces[0, 1, 2] = new EdgePiece(new[] { new Sticker(Faces.LEFT), new Sticker(Faces.UP) });
            _pieces[1, 1, 2] = new CentralPiece(new[] { new Sticker(Faces.UP) });
            _pieces[2, 1, 2] = new EdgePiece(new[] { new Sticker(Faces.RIGHT), new Sticker(Faces.UP) });

            // Third row
            _pieces[0, 2, 2] = new CornerPiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.UP), new Sticker(Faces.LEFT) });
            _pieces[1, 2, 2] = new EdgePiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.UP) });
            _pieces[2, 2, 2] = new CornerPiece(new[] { new Sticker(Faces.BACK), new Sticker(Faces.UP), new Sticker(Faces.RIGHT) });

            #endregion TOP LAYER
        }

        public override string ToString()
        {
            var str = string.Empty;

            str += string.Format("          | {0} {1} {2} | \r\n", 
                GetStickerColor(_pieces[0, 2, 2], Faces.UP), GetStickerColor(_pieces[1, 2, 2], Faces.UP), GetStickerColor(_pieces[2, 2, 2], Faces.UP));
            str += string.Format("          | {0} {1} {2} | \r\n", 
                GetStickerColor(_pieces[0, 1, 2], Faces.UP), GetStickerColor(_pieces[1, 1, 2], Faces.UP), GetStickerColor(_pieces[2, 1, 2], Faces.UP));
            str += string.Format("          | {0} {1} {2} | \r\n", 
                GetStickerColor(_pieces[0, 0, 2], Faces.UP), GetStickerColor(_pieces[1, 0, 2], Faces.UP), GetStickerColor(_pieces[2, 0, 2], Faces.UP));

            str += string.Format("{0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} \r\n",
                GetStickerColor(_pieces[0, 2, 2], Faces.LEFT), GetStickerColor(_pieces[0, 1, 2], Faces.LEFT), GetStickerColor(_pieces[0, 0, 2], Faces.LEFT),
                GetStickerColor(_pieces[0, 0, 2], Faces.FRONT), GetStickerColor(_pieces[1, 0, 2], Faces.FRONT), GetStickerColor(_pieces[2, 0, 2], Faces.FRONT),
                GetStickerColor(_pieces[2, 0, 2], Faces.RIGHT), GetStickerColor(_pieces[2, 1, 2], Faces.RIGHT), GetStickerColor(_pieces[2, 2, 2], Faces.RIGHT),
                GetStickerColor(_pieces[2, 2, 2], Faces.BACK), GetStickerColor(_pieces[1, 2, 2], Faces.BACK), GetStickerColor(_pieces[0, 2, 2], Faces.BACK));
            str += string.Format("{0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} \r\n",
                GetStickerColor(_pieces[0, 2, 1], Faces.LEFT), GetStickerColor(_pieces[0, 1, 1], Faces.LEFT), GetStickerColor(_pieces[0, 0, 1], Faces.LEFT),
                GetStickerColor(_pieces[0, 0, 1], Faces.FRONT), GetStickerColor(_pieces[1, 0, 1], Faces.FRONT), GetStickerColor(_pieces[2, 0, 1], Faces.FRONT),
                GetStickerColor(_pieces[2, 0, 1], Faces.RIGHT), GetStickerColor(_pieces[2, 1, 1], Faces.RIGHT), GetStickerColor(_pieces[2, 2, 1], Faces.RIGHT),
                GetStickerColor(_pieces[2, 2, 1], Faces.BACK), GetStickerColor(_pieces[1, 2, 1], Faces.BACK), GetStickerColor(_pieces[0, 2, 1], Faces.BACK));
            str += string.Format("{0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} \r\n",
                GetStickerColor(_pieces[0, 2, 0], Faces.LEFT), GetStickerColor(_pieces[0, 1, 0], Faces.LEFT), GetStickerColor(_pieces[0, 0, 0], Faces.LEFT),
                GetStickerColor(_pieces[0, 0, 0], Faces.FRONT), GetStickerColor(_pieces[1, 0, 0], Faces.FRONT), GetStickerColor(_pieces[2, 0, 0], Faces.FRONT),
                GetStickerColor(_pieces[2, 0, 0], Faces.RIGHT), GetStickerColor(_pieces[2, 1, 0], Faces.RIGHT), GetStickerColor(_pieces[2, 2, 0], Faces.RIGHT),
                GetStickerColor(_pieces[2, 2, 0], Faces.BACK), GetStickerColor(_pieces[1, 2, 0], Faces.BACK), GetStickerColor(_pieces[0, 2, 0], Faces.BACK));

            str += string.Format("          | {0} {1} {2} | \r\n",
                GetStickerColor(_pieces[0, 2, 0], Faces.DOWN), GetStickerColor(_pieces[1, 2, 0], Faces.DOWN), GetStickerColor(_pieces[2, 2, 0], Faces.DOWN));
            str += string.Format("          | {0} {1} {2} | \r\n",
                GetStickerColor(_pieces[0, 1, 0], Faces.DOWN), GetStickerColor(_pieces[1, 1, 0], Faces.DOWN), GetStickerColor(_pieces[2, 1, 0], Faces.DOWN));
            str += string.Format("          | {0} {1} {2} | \r\n",
                GetStickerColor(_pieces[0, 0, 0], Faces.DOWN), GetStickerColor(_pieces[1, 0, 0], Faces.DOWN), GetStickerColor(_pieces[2, 0, 0], Faces.DOWN));

            return str;
        }

        private static char GetStickerColor(Piece piece, Faces face)
        {
            return piece.Stickers.First(s => s.Face == face).Color.ToString()[0];
        }
    }
}
