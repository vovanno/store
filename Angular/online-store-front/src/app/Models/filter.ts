import { Genre } from './genre';
import { Platform } from './platform';
import { Publisher } from './publisher';
import { Price } from './price';

export class FilterModel {
    genres:number[] = null;
    platforms:number[] = null;
    publishers:number[] = null;
    isMostPopular:boolean = false;
    isMostCommented:boolean = false;
    byPriceAscending:boolean = false;
    byPriceDescending:boolean = false;
    byDateDescending:boolean = false;
    byDateAscending:boolean = false;
    priceFilter:Price = null;
    dateOfAdding:string;
}
