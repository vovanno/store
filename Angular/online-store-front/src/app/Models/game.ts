import { Genre } from './genre';
import { Platform } from './platform';

export class Game {
    gameId:number;
    name:string;
    description:string;
    publisherId:number;
    amountOfViews:number;
    price:number;
    genres:Genre[];
    dateOfAdding:string;
    platforms:Platform[];
}