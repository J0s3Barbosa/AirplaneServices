import { Injectable } from '@angular/core';

import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { AirPlaneModel } from './Shared/AirPlaneModel';
import { AirPlaneModelModel } from './Shared/AirPlaneModelModel';
import { AirPlaneAddModel } from './Shared/AirPlaneAddModel';
import { ApiResponse } from './Shared/ApiResponse';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const host = 'https://localhost:44344';
const endPointAirPlane = '/api/v1/airplane';
const endPointAirPlaneModel = '/api/v1/AirPlaneModel';
const apiUrlAirPlane = `${host}${endPointAirPlane}`;
const apiUrlAirPlaneModel = `${host}${endPointAirPlaneModel}`;

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  formData:AirPlaneAddModel;

  constructor(private http: HttpClient) { }

  getAirplanes(): Observable<AirPlaneModel[]> {
    return this.http.get<AirPlaneModel[]>(apiUrlAirPlane)
      .pipe(
        tap(airPlane => console.log(`fetched Airplanes =${airPlane}`)),
        catchError(this.handleError('getAirplanes', []))
      );
  }

  getAirplane(id: string): Observable<AirPlaneModel> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.get<AirPlaneModel>(url).pipe(
      tap(airPlane =>
        console.log(`fetched Airplane id=${id} airPlane=${airPlane.code}`)
      ),
      catchError(this.handleError<AirPlaneModel>(`getAirplane id=${id}`))
    );
  }

  addAirplane(airPlaneAddModel: any): Observable<AirPlaneAddModel> {

    let options = {
      headers: new HttpHeaders()
        .set("Content-Type", "application/json")
        .set("Accept", "application/json")
    };

    // var obj =  {
    //   "code": "string",
    //   "model": {
    //     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    //   },
    //   "numberOfPassengers": 0
    // }

    var obj = {
      code: airPlaneAddModel.code,
      model: airPlaneAddModel.model,
      numberOfPassengers: airPlaneAddModel.numberOfPassengers
    };
    return this.http.post<AirPlaneAddModel>(apiUrlAirPlane, obj, options).pipe(
      tap((airPlaneRes: AirPlaneAddModel) => console.log(`added Airplane w/ code=${airPlaneRes.code}`)),
      catchError(this.handleError<AirPlaneAddModel>('addAirplane'))
    );
  }

  postAirplane(airPlaneAddModel: AirPlaneAddModel): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(apiUrlAirPlane, airPlaneAddModel);
  }


  updateAirplane(id: string, airPlane: any): Observable<any> {
    const url = `${apiUrlAirPlane}/${id}`;
    // return this.http
    // .put(url, airPlane, httpOptions)
    // .pipe(
    //   tap(_ => console.log(`updated Airplane id=${id}`)),
    //   catchError(this.handleError<any>('updateAirplane'))
    // );

    return this.http
      .put<any>(`${url}`, airPlane, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      })
      .pipe(
        catchError(this.handleError)
      );


  }
  putAirPlane(id: string, airPlane: any) {
    const url = `${apiUrlAirPlane}/${id}`;
    const obj = {
      code: airPlane.code,
      model: airPlane.model,
      numberOfPassengers: airPlane.numberOfPassengers
    };
    return this
      .http
      .put(`${url}`, obj)
      .subscribe(res => console.log(`res ${res}`)
        , catchError(this.handleError)
      )
      ;
  }

  deleteAirplane(id: string): Observable<AirPlaneModel> {
    const url = `${apiUrlAirPlane}/${id}`;
    return this.http.delete<AirPlaneModel>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted Airplane id=${id}`)),
      catchError(this.handleError<AirPlaneModel>('deleteAirplane'))
    );
  }

  getAirPlaneModelModels(): Observable<AirPlaneModelModel[]> {
    return this.http.get<AirPlaneModelModel[]>(apiUrlAirPlaneModel)
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



