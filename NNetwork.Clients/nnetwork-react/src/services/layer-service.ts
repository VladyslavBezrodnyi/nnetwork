import { LayerType } from "../enums/layer-type";

const layerService = (layerType: LayerType): string[] => {
    switch (layerType) {
        case LayerType.Conv2D: {
            return ["filters", "kernel_size"];
        }
        case LayerType.Dense: {
            return ["units"];
        }
        case LayerType.Dropout: {
            return ["rate"];
        }
        case LayerType.Flatten: {
            return [];
        }
        case LayerType.Input: {
            return ["shape"];
        }
        case LayerType.MaxPooling2D: {
            return ["pool_size"];
        }
        case LayerType.UpSampling2D: {
            return ["size"];
        }
        case LayerType.LeakyReLU: {
            return ["alpha"];
        }
        case LayerType.PReLU: {
            return [];
        }
        case LayerType.ReLU: {
            return [];
        }
        case LayerType.ELU: {
            return ["alpha"];
        }
        case LayerType.Softmax: {
            return ["axis"];
        }
        default: {
            return [];
        }
    }
}

export default layerService;