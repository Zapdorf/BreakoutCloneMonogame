using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType;

namespace Pong.Graphics.TrailRenderer
{
    public class TrailSegment
    {
        private VertexPositionColor[] _vertices;
        private BasicEffect _basicEffect;
        private VertexBuffer _vertexBuffer;

        private SpriteBatch _spriteBatch;

        /*
         * Objectives
         * - layering
         * - vector 2 coords to these coords
         * 
         */

        public TrailSegment(GraphicsDevice _graphics)
        {
            var zvalue = 0.6f;

            _spriteBatch = new SpriteBatch(_graphics);


            // Define the triangle vertices with positions and color (optional)
            _vertices = new VertexPositionColor[3]
            {
                //new VertexPositionColor(new Vector3(Globals.ScreenWidth/2, 0, zvalue), Color.Red), 
                //new VertexPositionColor(new Vector3(Globals.ScreenWidth, Globals.ScreenHeight, zvalue), Color.Green),
                //new VertexPositionColor(new Vector3(0, Globals.ScreenHeight, zvalue), Color.Blue),

                new VertexPositionColor(new Vector3(0, -1, zvalue), Color.Red),
                new VertexPositionColor(new Vector3(1, 0, zvalue), Color.Green),
                new VertexPositionColor(new Vector3(-1, 0, zvalue), Color.Blue),

                // coords range from -1 to 1 in both dimensions
                // for X, ((X / ScreenWidth) * 2) - 1
            };


            _basicEffect = new(_spriteBatch.GraphicsDevice);
            _basicEffect.VertexColorEnabled = true;


            _vertexBuffer = new VertexBuffer(_spriteBatch.GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_vertices);
        }

        public void Draw()
        {
            _spriteBatch.GraphicsDevice.SetVertexBuffer(_vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            _spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _spriteBatch.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }

        }

        private void ConvertScreenCoordsToVertices()
        {

        }
    }
}
