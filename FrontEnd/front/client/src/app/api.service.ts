import { Injectable } from '@angular/core';

import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { AirPlaneModel } from './Shared/AirPlaneModel';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
const host = 'https://localhost:44344';
const endPointAirPlane = '/v1/AirPlane';
const endPointAirPlaneModel = '/v1/AirPlaneModel';
const apiUrlAirPlane = `${host}${endPointAirPlane}`;

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  getAirplanes (): Observable<AirPlaneModel[]> {
    return this.http.get<AirPlaneModel[]>(apiUrlAirPlane)
      .pipe(
        tap(heroes => console.log('fetched Airplanes')),
        catchError(this.handleError('getAirplanes', []))
      );
  }
  
  getAirplane(id: number): Observable<AirPlaneModel> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.get<AirPlaneModel>(url).pipe(
      tap(_ => console.log(`fetched Airplane id=${id}`)),
      catchError(this.handleError<AirPlaneModel>(`getAirplane id=${id}`))
    );
  }
  
  addAirplane (airPlane: any): Observable<AirPlaneModel> {
    return this.http.post<AirPlaneModel>(apiUrlAirPlane, airPlane, httpOptions).pipe(
      tap((airPlaneRes: AirPlaneModel) => console.log(`added Airplane w/ id=${airPlaneRes.id}`)),
      catchError(this.handleError<AirPlaneModel>('addAirplane'))
    );
  }
  
  updateAirplane (id: number, airPlane: any): Observable<any> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.put(url, airPlane, httpOptions).pipe(
      tap(_ => console.log(`updated Airplane id=${id}`)),
      catchError(this.handleError<any>('updateAirplane'))
    );
  }
  
  deleteAirplane (id: number): Observable<AirPlaneModel> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.delete<AirPlaneModel>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted Airplane id=${id}`)),
      catchError(this.handleError<AirPlaneModel>('deleteAirplane'))
    );
  }

  
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }


}



