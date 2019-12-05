using System;
using System.Collections.Generic;
using System.Text;

namespace ink_recognizer
{
    public class Alternate
    {
        public string category { get; set; }
        public string recognizedString { get; set; }
    }

    public class BoundingRectangle
    {
        public double height { get; set; }
        public double topX { get; set; }
        public double topY { get; set; }
        public double width { get; set; }
    }

    public class RotatedBoundingRectangle
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class RecognitionUnit
    {
        public List<Alternate> alternates { get; set; }
        public BoundingRectangle boundingRectangle { get; set; }
        public string category { get; set; }
        public string @class { get; set; }
        public int id { get; set; }
        public int parentId { get; set; }
        public string recognizedText { get; set; }
        public List<RotatedBoundingRectangle> rotatedBoundingRectangle { get; set; }
        public List<int> strokeIds { get; set; }
        public List<int?> childIds { get; set; }
    }

    public class RootObject
    {
        public List<RecognitionUnit> recognitionUnits { get; set; }
    }
}
