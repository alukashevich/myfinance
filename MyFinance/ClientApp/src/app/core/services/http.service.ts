import { Injectable } from '@angular/core';
import { Http, URLSearchParams, RequestOptions } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResponseResult } from '../../models';

@Injectable()
export class HttpService {
  private headers: Headers;

  constructor(private http: Http) {
    this.headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
  }

  get(path: string) {
    return this.http.get(path, { headers: this.headers })
      .pipe(map((resp: Response) => {
        let data = resp.json();
        return this.formResponseResult(data);
      }));
  }

  post(path: string, data: any): Observable<any> {
    return this.http.post(path, data, { headers: this.headers })
      .pipe(map((resp: Response) => {
        let data = resp.json();
        return this.formResponseResult(data);
      }));
  }

  put(path: string, data: any): Observable<any> {
    return this.http.put(path, data, { headers: this.headers })
      .pipe(map((resp: Response) => {
        let data = resp.json();
        return this.formResponseResult(data);
      }));
  }

  delete(path: string) {
    return this.http.delete(path, { headers: this.headers })
      .pipe(map((resp: Response) => {
        let data = resp.json();
        return this.formResponseResult(data);
      }));
  }

  formResponseResult(data: any): ResponseResult {
    var result = new ResponseResult();
    result.IsSuccess = data.state != null && data.state == '1';
    result.Message = data.message;
    result.Data = data.returnValue;
    return result;
  }
}
