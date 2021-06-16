import { NetworkCreationResultDto, NetworkDto, NetworkInfoDto, NetworkInitializerDto, PredictionDto, PredictionResultDto } from './dto-types';

export interface HeaderProps {
  history: any
}

export interface HomeProps {
  history: any,
}

export interface AppProps {
}

export interface CreateNetworkProps {
  history: any,
  networkCreationResult: NetworkCreationResultDto,
  createNetwork(initial: NetworkInitializerDto): void
}

export interface NetworksProps {
  history: any,
  networks: NetworkDto[],
  getAllNetworks(): void
}

export interface NetworkInfoProps {
  history: any,
  match: any,
  network: NetworkInfoDto,
  predictionResult: PredictionResultDto,
  getNetworkById(networkId: string): void,
  predictImage(predictionDto: PredictionDto): void
}