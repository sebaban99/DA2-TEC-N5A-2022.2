import { ActorBasicModel } from "./ActorBasicModel";

export class MovieDetailModel {
    id: number;
    title: string;
    description: string;
    actors: ActorBasicModel[];
}