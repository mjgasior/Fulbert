using System;

namespace Fulbert.BLL.ApplicationModels.Events
{
    public class ModelChangedArgs : EventArgs
    {
        private Guid _modelId;
        public Guid Id
        {
            get { return _modelId; }
        }

        public ModelChangedArgs(Guid modelId)
        {
            _modelId = modelId;
        }
    }
}
