import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './Components/main-page/main-page.component';
import { GamesPageComponent } from './Components/games-page/games-page.component';
import { GameService } from './Services/game.service';
import { HttpClientModule } from '@angular/common/http';
import { GameItemComponent } from './Components/game-item/game-item.component';
import { GameDetailsComponent } from './Components/game-details/game-details.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    GamesPageComponent,
    GameItemComponent,
    GameDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    GameService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
