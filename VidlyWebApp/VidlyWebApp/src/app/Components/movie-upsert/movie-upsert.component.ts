import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { MovieBasicModel } from 'src/app/Models/MovieBasicModel';
import { MovieDetailModel } from 'src/app/Models/MovieDetailModel';
import { MovieService } from 'src/app/Services/Movies/movie.service';

@Component({
  selector: 'app-movie-upsert',
  templateUrl: './movie-upsert.component.html',
  styleUrls: ['./movie-upsert.component.css']
})
export class MovieUpsertComponent implements OnInit, OnChanges {

  @Input() movieDisplayed: MovieDetailModel;
  @Output() updatedMovieEmitter = new EventEmitter<MovieDetailModel>();
  title: string = "";
  description: string = "";

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    if (this.movieDisplayed) {
      this.title = this.movieDisplayed.title;
      this.description = this.movieDisplayed.description;
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['movieDisplayed'] && changes['movieDisplayed'].currentValue) {
      const selectedMovie = changes['movieDisplayed'].currentValue;
      this.title = selectedMovie.title;
      this.description = selectedMovie.description;
    }
  }

  onSubmitUpdate(): void {
    this.movieDisplayed!.title = this.title;
    this.movieDisplayed!.description = this.description;
    this.updatedMovieEmitter.emit(this.movieDisplayed);
  }

  onSubmitCreate(): void {
    let newMovie = new MovieDetailModel();
    newMovie.title = this.title;
    newMovie.description = this.description;
    this.movieService.postMovie(newMovie)
      .subscribe({
        next: _ => alert('Movie created successfully!'),
        error: err => alert(`Error creating movie ${newMovie.title}`)
      });
  }

}
