using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// CNTKGraph

namespace MNIST.Client.UWP
{
    public sealed class CNTKGraphModelInput
    {
        public VideoFrame Input3 { get; set; }
    }

    public sealed class CNTKGraphModelOutput
    {
        public IList<float> Plus258_Output_0 { get; set; }
        public CNTKGraphModelOutput()
        {
            this.Plus258_Output_0 = new List<float>();
        }
    }

    public sealed class CNTKGraphModel
    {
        private LearningModelPreview learningModel;
        public static async Task<CNTKGraphModel> CreateCNTKGraphModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            CNTKGraphModel model = new CNTKGraphModel();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<CNTKGraphModelOutput> EvaluateAsync(CNTKGraphModelInput input) {
            CNTKGraphModelOutput output = new CNTKGraphModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("Input3", input.Input3);
            binding.Bind("Plus258_Output_0", output.Plus258_Output_0);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
