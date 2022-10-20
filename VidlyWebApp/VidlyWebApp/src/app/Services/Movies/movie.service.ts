import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { MovieDetailModel } from 'src/app/Models/MovieDetailModel';
import { MovieBasicModel } from 'src/app/Models/MovieBasicModel';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private URL: string = environment.API_URL + "/movies";

  constructor(private httpService: HttpClient) { }

  getMovieById(movieId: string): Observable<MovieDetailModel> {
    return this.httpService.get<MovieDetailModel>(this.URL + '/' + movieId)
      .pipe(
        catchError(error => {
          console.log('Caught in CatchError. Throwing error')
          return throwError(() => error)
        })
      );
  }

  getAllMovies(): Observable<MovieBasicModel[]> {
    return this.httpService.get<MovieBasicModel[]>(this.URL).pipe(
      catchError(error => {
        console.log('Caught in CatchError. Throwing error')
        return throwError(() => error)
      })
    );
  }

  postMovie(newMovie: MovieDetailModel): Observable<MovieDetailModel> {
    return this.httpService.post<MovieDetailModel>(this.URL, newMovie)
      .pipe(
        catchError(error => {
          console.error('HTTP Error: ', error)
          return throwError(() => error)
        })
      );
  }

  putMovie(updatedMovie: MovieDetailModel, movieId: string): Observable<MovieDetailModel> {
    return this.httpService.put<MovieDetailModel>(this.URL + '/' + movieId, updatedMovie)
      .pipe(
        catchError(error => {
          console.log('Caught in CatchError. Throwing error')
          return throwError(() => error)
        })
      );
  }


}
