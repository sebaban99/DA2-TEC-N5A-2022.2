import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { MovieUpsertComponent } from './Components/movie-upsert/movie-upsert.component';
import { MoviesComponent } from './Components/movies/movies.component';
import { AuthenticationGuard } from './Guards/authentication.guard';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'movies', component: MoviesComponent },
  // Can activate para guards
  { path: 'create-movie', component: MovieUpsertComponent,  canActivate: [AuthenticationGuard]},
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
