import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './Components/main-page/main-page.component';
import { GamesPageComponent } from './Components/games-page/games-page.component';
import { FilterComponent } from './Components/filter/filter.component';
import { ClientLoginComponent } from './Components/Authentication/client-login/client-login.component';
import { CreateGameComponent } from './Components/create-game/create-game.component';

const routes: Routes = [
  {path:'Main', component: MainPageComponent},
  {path:'Games', component: GamesPageComponent},
  {path: 'Main', component:MainPageComponent},
  {path: 'ClientLogin', component: ClientLoginComponent},
  {path: 'SupportLogin', component: ClientLoginComponent},
  {path: 'CreateGame', component: CreateGameComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
