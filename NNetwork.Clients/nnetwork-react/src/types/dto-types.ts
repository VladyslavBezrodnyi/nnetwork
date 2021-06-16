import { Parameters } from '../models/layer-parameters';
import { LayerType } from './../enums/layer-type';

export interface NetworkBaseDto {
    title: string,
    description: string
}

export interface NetworkInitializerDto extends NetworkBaseDto {
    layers: LayerDto[]
}

export interface LayerDto {
    id: Number;
    layerType: LayerType;
    parameters: Parameters;
    transitions: Number[]
}

export interface NetworkDto extends NetworkBaseDto {
    id: string,
}

export interface NetworkInfoDto extends NetworkBaseDto {
    id: string,
    plotImage: string,
}

export interface NetworkCreationResultDto {
    plotImage: string,
}

export interface PredictionDto {
    file: string,
    networkId: string
}

export interface PredictionResultDto {
    probabilities: Number[],
    index: Number
}