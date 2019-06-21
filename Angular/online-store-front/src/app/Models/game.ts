import { Genre } from './genre';

export class Game {
    gameId:number;
    name:string;
    description:string;
    publisherId:number;
    amountOfViews:number;
    price:number;
    genres:Genre[];
    dateOfAdding:Date;
}