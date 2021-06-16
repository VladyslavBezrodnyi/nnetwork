import { NetworkDto, NetworkCreationResultDto, NetworkInfoDto, PredictionResultDto } from './dto-types';

export interface CreateNetworkState {
    networkCreationResult: NetworkCreationResultDto
}

export interface NetworksState {
    networks: NetworkDto[],
}

export interface NetworkState {
    network: NetworkInfoDto,
    predictionResult: PredictionResultDto,
}