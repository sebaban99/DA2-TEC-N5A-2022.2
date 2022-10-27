import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './Components/menu/menu.component';
import { MoviesComponent } from './Components/movies/movies.component';
import { HomeComponent } from './Components/home/home.component';
import { MovieUpsertComponent } from './Components/movie-upsert/movie-upsert.component';
import { CustomPipe } from './Pipes/custom.pipe';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    MoviesComponent,
    HomeComponent,
    MovieUpsertComponent,
    CustomPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
