export interface Dictionary<T> {
    [Key: string]: T;
}

export interface Parameters {
    filters: Number;
    kernel_size: Number[];
    units: Number;
    rate: Number; //double
    shape: Number[];
    pool_size: Number[];
    size: Number[];
    alpha: Number; //float
    axis: Number;
}

/*
export interface Parameter {
}

export interface ActivationParameter extends Parameter {
    act: string;
}
export interface Conv2DParameter extends Parameter {
    filters: Number;
    kernel_size: Tuple2;
}
export interface DenseParameter extends Parameter {
    units: Number;
}
export interface DropoutParameter extends Parameter {
    rate: Number; //double
}
export interface FlattenParameter extends Parameter {
}
export interface InputParameter extends Parameter {
    shape: Tuple3;
}
export interface MaxPooling2DParameter extends Parameter {
    pool_size: Tuple2;
}
export interface UpSampling2DParameter extends Parameter {
    size: Tuple2;
}
export interface LeakyReLUParameter extends Parameter {
    alpha: Number; //float
}
export interface PReLUParameter extends Parameter {
}
export interface ReLUParameter extends Parameter {
}
export interface ELUParameter extends Parameter {
    alpha: Number; //float
}
export interface SoftmaxParameter extends Parameter {
    axis: Number;
}
*/