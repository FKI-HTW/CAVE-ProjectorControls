using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace CaveCalibrator.Cave.Protocol
{
    public class CaveProtocol
    {
        /// <summary>/
        /// The localhost address.
        /// </summary>
        public static string localhost = "127.0.0.1";

        /// <summary>
        /// The port used to establish a connection between
        /// the calibration editor and application.
        /// </summary>
        public static ushort port = 55555;

        /// <summary>
        /// The default directory to read from and save to calibration files.
        /// </summary>
        public static string defaultFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    /// <summary>
    /// Available calibration messages.
    /// </summary>
    public static class Message
    {
        public const int Disconnect = 1;

        public const int Sync = 10;

        public const int Calibration = 11;

        public const int ShowHelpers = 30;

        public const int LockCameras = 31;

        public const int OnlyCameraDisplay = 32;
    }


    /// <summary>
    /// Defines the output target of a <see cref="Camera"/>
    /// component. 
    /// </summary>
    [Serializable]
    public enum OutputTarget
    {
        Display,
        SplitHorizontal,
        SplitVertical
    }

    /// <summary>
    /// Helping enum to identify a quad corner.
    /// </summary>
    public enum QuadCorner
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    /// <summary>
    /// A quad defined by four corners.
    /// </summary>
    [Serializable]
    public struct Quad
    {
        public Vector2f topLeft;

        public Vector2f topRight;

        public Vector2f bottomLeft;

        public Vector2f bottomRight;

        public Vector2f GetCorner(QuadCorner corner)
        {
            switch (corner)
            {
                case QuadCorner.TopLeft: return topLeft;
                case QuadCorner.TopRight: return topRight;
                case QuadCorner.BottomLeft: return bottomLeft;
                case QuadCorner.BottomRight: return bottomRight;
                default:
                    Debug.WriteLine("Unknown quad corner: " + corner);
                    return new Vector2f();
            }
        }

        public Vector2f SetCorner(QuadCorner corner, Vector2f position)
        {
            switch (corner)
            {
                case QuadCorner.TopLeft: return topLeft = position;
                case QuadCorner.TopRight: return topRight = position;
                case QuadCorner.BottomLeft: return bottomLeft = position;
                case QuadCorner.BottomRight: return bottomRight = position;
                default:
                    Debug.WriteLine("Unknown quad corner: " + corner);
                    return position;
            }
        }

        public static Quad Unitary() => new Quad
        {
            topLeft = new Vector2f(-1f, 1f),
            topRight = new Vector2f(1f, 1f),
            bottomLeft = new Vector2f(-1f, -1f),
            bottomRight = new Vector2f(1f, -1f)
        };
    }

    public struct Vector2f
    {
        public float x;
        public float y;

        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Contains calibration data and information of
    /// a single camera.
    /// </summary>
    [Serializable]
    public struct Calibration
    {
        public string name;

        public int virtualDisplay;

        public float viewportSize;

        public OutputTarget outputTarget;

        public bool projectionCorrection;

        public Quad projectionQuad;
    }


    /// <summary>
    /// Stores the calibration data of multiple cameras.
    /// Used for sending and receiving calibrations, as well as
    /// for loading and saving calibrations from or to disk.
    /// </summary>
    [Serializable]
    public struct Package
    {
        [JsonIgnore]
        public bool isEmpty => this.calibrations == null || calibrations.Length == 0;

        public string timestamp;

        public OutputTarget outputTarget;

        public int highestVirtualDisplay;

        public Calibration[] calibrations;

        public Package(DateTime timestamp, OutputTarget outputTarget,
            int highestActiveDisplay, Calibration[] calibrations)
        {
            this.timestamp = timestamp.ToString("o");
            this.outputTarget = outputTarget;
            this.highestVirtualDisplay = highestActiveDisplay;
            this.calibrations = calibrations;
        }
    }
}
