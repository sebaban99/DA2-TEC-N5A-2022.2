import { Component, OnInit } from '@angular/core';
import { MovieBasicModel } from 'src/app/Models/MovieBasicModel';
import { MovieDetailModel } from 'src/app/Models/MovieDetailModel';
import { MovieService } from 'src/app/Services/Movies/movie.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  movies: MovieBasicModel[] = [];
  selectedMovieId: string;
  selectedMovie: MovieDetailModel;

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.getMovies();
  }

  getMovies(): void {
    this.movieService.getAllMovies().subscribe(movies => this.movies = movies);
  }

  getMovieById(): void {
    this.movieService.getMovieById(this.selectedMovieId)
      .subscribe((movie: MovieDetailModel) => {
        this.selectedMovie = movie;
        console.log("selectedMovie", this.selectedMovie); //Solo para mostrar como cambia en el ejemplo, borrar este console.log
      })
  }

  updateMovie(event: any): void {
    this.movieService.putMovie(event, this.selectedMovieId)
      .subscribe({
        next: res => {
          alert('Movie updated successfully!');
          this.getMovies();
        },
        error: err => alert(`Error updating movie ${event.title}`)
      });
  }

}
