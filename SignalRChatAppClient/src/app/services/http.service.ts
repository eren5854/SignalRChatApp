import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  get(api: string, callBack: (res:any) => void){
    this.http.get(`https://localhost:7212/api/${api}`, {
      headers: {
        "Authorization": "Bearer " + this.auth.token
      }
    }).subscribe({
      next: (res: any) => {
        callBack(res);
      },
      error: (err: HttpErrorResponse) => {
        if (!err.error.isSuccess) {
          console.log(err.error.errorMessages);
        }
        else{
          console.log(err);
        }
      }
    });
  }

  get2<T>(api: string, callBack: (res:T) => void){
    this.http.get<T>(`https://localhost:7212/api/${api}`, {
      headers: {
        "Authorization": "Bearer " + this.auth.token
      }
    }).subscribe({
      next: (res: any) => {
        callBack(res);
      },
      error: (err: HttpErrorResponse) => {
        if (!err.error.isSuccess) {
          console.log(err.error.errorMessages);
        }
        else{
          console.log(err);
        }
      }
    });
  }

  post(api: string, body:any,callBack: (res:any)=> void) {
    this.http.post(`https://localhost:7212/api/${api}`,body, {
      headers: {
        "Authorization": "Bearer " + this.auth.token
      }
    }).subscribe({
      next: (res: any) => {
        callBack(res);
      },
      error: (err: HttpErrorResponse) => {
        if (!err.error.isSuccess) {
          console.log(err.error.errorMessages);
        }
        else{
          console.log(err);
        }
      }
    });
  }

  post2<T>(api: string, body:any,callBack: (res:T)=> void) {
    this.http.post<T>(`https://localhost:7212/api/${api}`,body, {
      headers: {
        "Authorization": "Bearer " + this.auth.token
      }
    }).subscribe({
      next: (res: any) => {
        callBack(res);
      },
      error: (err: HttpErrorResponse) => {
        if (!err.error.isSuccess) {
          console.log(err.error.errorMessages);
        }
        else{
          console.log(err);
        }
      }
    });
  }
}