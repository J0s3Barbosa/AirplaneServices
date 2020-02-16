import { Injectable } from '@angular/core';

import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { AirPlaneModel } from './Shared/AirPlaneModel';
import { ApiResponse } from './Shared/ApiResponse';
import { AirPlaneAdd } from './Shared/AirPlaneAdd';
import { AirPlane } from './Shared/AirPlane';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const host = 'https://localhost:5000';
const endPointAirPlane = '/api/v1/airplane';
const endPointAirPlaneModel = '/api/v1/AirPlaneModel';
const apiUrlAirPlane = `${host}${endPointAirPlane}`;
const apiUrlAirPlaneModel = `${host}${endPointAirPlaneModel}`;

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  formData:AirPlaneAdd;

  constructor(private http: HttpClient) { }

  getAirplanes(): Observable<AirPlane[]> {
    return this.http.get<AirPlane[]>(apiUrlAirPlane)
      .pipe(
        tap(airPlane => console.log(`fetched Airplanes =${airPlane}`)),
        catchError(this.handleError('getAirplanes', []))
      );
  }

  getAirplane(id: string): Observable<AirPlane> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.get<AirPlane>(url).pipe(
      tap(airPlane =>
        console.log(`fetched Airplane id= ${id} airPlane.model.name= ${airPlane.model.name}`)
      ),
      catchError(this.handleError<AirPlane>(`getAirplane id= ${id}`))
    );
  }

  addAirplane(airPlaneAddModel: any): Observable<AirPlaneAdd> {

    let options = {
      headers: new HttpHeaders()
        .set("Content-Type", "application/json")
        .set("Accept", "application/json")
    };
  
    var obj = {
      code: airPlaneAddModel.code,
      model: airPlaneAddModel.model,
      numberOfPassengers: airPlaneAddModel.numberOfPassengers
    };
    return this.http.post<AirPlaneAdd>(apiUrlAirPlane, obj, options).pipe(
      tap((airPlaneRes: AirPlaneAdd) => console.log(`added Airplane w/ code=${airPlaneRes.code}`)),
      catchError(this.handleError<AirPlaneAdd>('addAirplane'))
    );
  }

  postAirplane(airPlaneAddModel: AirPlaneAdd): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(apiUrlAirPlane, airPlaneAddModel);
  }

  updateAirplane(id: string, airPlane: any): Observable<any> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http
    .put(url, airPlane, httpOptions)
    .pipe(
      tap(_ => console.log(`updated Airplane id=${id}`)),
      catchError(this.handleError<any>('updateAirplane'))
    );

  }
 
  deleteAirplane(id: string): Observable<AirPlane> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.delete<AirPlane>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted Airplane id=${id}`)),
      catchError(this.handleError<AirPlane>('deleteAirplane'))
    );
  }

  getAirPlaneModels(): Observable<AirPlaneModel[]> {
    return this.http.get<AirPlaneModel[]>(apiUrlAirPlaneModel)
      .pipe(
        tap(airPlaneModelModel => console.log(`fetched Airplanes =${airPlaneModelModel}`)),
        catchError(this.handleError('getAirplanes', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }


}



