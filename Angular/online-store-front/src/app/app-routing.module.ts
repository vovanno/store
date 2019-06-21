import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './Components/main-page/main-page.component';
import { GamesPageComponent } from './Components/games-page/games-page.component';

const routes: Routes = [
  {path:'Main', component: MainPageComponent},
  {path:'Games', component: GamesPageComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
