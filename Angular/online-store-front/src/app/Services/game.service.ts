import { Injectable } from '@angular/core';
import { Game } from '../Models/game';
import { HttpClient, HttpHeaders, HttpParams} from '@angular/common/http'
import { FilterModel } from '../Models/filter';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  gameData: Game[];
  constructor(private http: HttpClient) { }

  private Url = "https://localhost:44326";

  GetGames(){
    return this.http.get(this.Url+"/api/games");
  }

  AddGame(game:Game){
    return this.http.post(this.Url + "/api/games", game);
  }

  GetGamesWithFilters(filter: FilterModel){
    return this.http.post(this.Url+"/api/games/filters", filter);
  }

}
