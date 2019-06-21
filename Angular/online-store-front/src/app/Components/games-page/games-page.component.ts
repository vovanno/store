import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/Services/game.service';
//import { runInThisContext } from 'vm';
import { Game } from 'src/app/Models/game';

@Component({
  selector: 'app-games-page',
  templateUrl: './games-page.component.html',
  styleUrls: ['./games-page.component.css']
})
export class GamesPageComponent implements OnInit {

  constructor(private gameService: GameService) { }

  ngOnInit() {
     this.getGames();
  }

  getGames() {
    this.gameService.GetGames().subscribe((data: any) => {
      this.gameService.gameData = data;
    }, () => console.log("data didnt received"))
  }

}
