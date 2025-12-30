namespace MetinBank.Forms.Properties
{
    internal static class Resources
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MetinBank.Forms.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        internal static global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }
        
        // Placeholder SVG resources - DevExpress has built-in icons
        internal static byte[] bo_contact { get { return null; } }
        internal static byte[] business { get { return null; } }
        internal static byte[] exporttopdf { get { return null; } }
        internal static byte[] contentarrangeinrows { get { return null; } }
        internal static byte[] send { get { return null; } }
        internal static byte[] bo_sale_item { get { return null; } }
        internal static byte[] bo_task { get { return null; } }
        internal static byte[] bo_attention { get { return null; } }
    }
}
