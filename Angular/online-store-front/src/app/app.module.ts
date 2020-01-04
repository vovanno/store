import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './Components/main-page/main-page.component';
import { GamesPageComponent } from './Components/games-page/games-page.component';
import { GameService } from './Services/game.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { GameItemComponent } from './Components/game-item/game-item.component';
import { GameDetailsComponent } from './Components/game-details/game-details.component';
import { GenreService } from './Services/genre.service';
import { PublisherService } from './Services/publisher.service';
import { PlatformService } from './Services/platform.service';
import { FilterComponent } from './Components/filter/filter.component';
import { ClientLoginComponent } from './Components/Authentication/client-login/client-login.component';
import { AuthenticateService } from './Services/authenticate.service';
import { AuthInterceptor } from './Services/interceptor.service';
import { CreateGameComponent } from './Components/create-game/create-game.component';


@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    GamesPageComponent,
    GameItemComponent,
    GameDetailsComponent,
    FilterComponent,
    ClientLoginComponent,
    CreateGameComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule

  ],
  providers: [
    GameService,
    GenreService,
    PublisherService,
    PlatformService,
    AuthenticateService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass:AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
