import { Injectable } from '@angular/core';
import { Game } from '../Models/game';
import { HttpClient} from '@angular/common/http'

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
}
