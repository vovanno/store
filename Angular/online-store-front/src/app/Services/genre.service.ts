import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Genre } from '../Models/genre';
import { extendedGenre } from '../Models/exetndedGenre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  genreData: Genre[];
  extendedGenre: extendedGenre[];

  private Url = "https://localhost:44326";

  constructor(private http: HttpClient) { }

  public getGenres(){
    return this.http.get(this.Url + "/api/genres");
  }
}
