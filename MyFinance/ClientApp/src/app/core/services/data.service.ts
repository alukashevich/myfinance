import { Injectable, Type } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from '../services'
import { Login } from '../../models';
import { Register } from '../../models';
import { ResponseResult } from '../../models';
import { Calc } from '../../models/calc.model';
import { SpecialPersentage } from '../../models/specialPersentage';
import { MonthPayment } from '../../models/monthPayment';

@Injectable()
export class DataService {
  public url: string = "https://localhost:44360"
  //CRUD
  constructor(private httpService: HttpService) {
  }

  public post(itemType: Type<any>, method: string,item: any): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.post(`${this.url}/${path}/${method}`, item);
  }

  public create(itemType: Type<any>, item: any): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.post(`${this.url}/${path}`, item);
  }

  public update(itemType: Type<any>, item: any): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.put(`${this.url}/${path}/${item.UID}`, item);
  }

  public delete(itemType: Type<any>, id: string): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.delete(`${this.url}/${path}/${id}`);
  }

  public get(itemType: Type<any>, id: string): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.get(`${this.url}/${path}/${id}`);
  }

  public getAll(itemType: Type<any>): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.get(`${this.url}/${path}`);
  }

  //GetForParent

  public getAllForParent(itemType: Type<any>, parentType: Type<any>, id: string) {
    let path = this.getPath(itemType);
    let parentPath = this.getName(parentType);
    return this.httpService.get(`${this.url}/${path}/for${parentPath}/${id}`);
  }

  //Add&Remove&UpdateLinks

  public addLink(firstItemType: Type<any>, firstItemId: string, secondItemType: Type<any>, secondItemId: string) {
    let path = this.getPath(firstItemType);
    let secondItemName = this.getName(secondItemType);
    return this.httpService.post(`${this.url}/${path}/${firstItemId}/add${secondItemName}/${secondItemId}`, null);
  }

  public removeLink(firstItemType: Type<any>, firstItemId: string, secondItemType: Type<any>, secondItemId: string) {
    let path = this.getPath(firstItemType);
    let secondItemName = this.getName(secondItemType);
    return this.httpService.post(`${this.url}/${path}/${firstItemId}/remove${secondItemName}/${secondItemId}`, null);
  }

  public updateLink(firstItemType: Type<any>, firstItemId: string, secondItemType: Type<any>, secondItemId: string, data: any) {
    let path = this.getPath(firstItemType);
    let secondItemName = this.getName(secondItemType);
    return this.httpService.post(`${this.url}/${path}/${firstItemId}/update${secondItemName}/${secondItemId}`, data);
  }

  //unique

  public getAvailableLanguages(): Observable<ResponseResult> {
    let path = 'api/Languages/getAvailable';
    return this.httpService.get(`${this.url}/${path}`);
  }

  public getDashboard(): Observable<ResponseResult> {
    let path = 'api/Dashboard';
    return this.httpService.get(`${this.url}/${path}`);
  }

  public enable(itemType: Type<any>, id: string): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.post(`${this.url}/${path}/${id}/enable`, null);
  }

  public disable(itemType: Type<any>, id: string): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.post(`${this.url}/${path}/${id}/disable`, null);
  }

  public customMethod(itemType: Type<any>, id: string, name: string): Observable<ResponseResult> {
    let path = this.getPath(itemType);
    return this.httpService.post(`${this.url}/${path}/${id}/${name}`, null);
  }

  //Account - Login

  public login(loginInfo: Login): Observable<ResponseResult> {
    let api = 'api';
    return this.httpService.post(`${this.url}/${api}/Users/Login/`, loginInfo);
  }

  //Inner methods

  private getPath(itemType: Type<any>) {
    let api = 'api';

    switch (itemType) {
      case Login: return `${api}/login`;
      case Register: return `${api}/register`;
      case Calc: return `${api}/credit`;
      case MonthPayment: return `${api}/monthpayment`;
      case SpecialPersentage: return `${api}/specialPercentage`;

      default: throw new Error(`DataService error: Can't find path for type ${itemType}`);
    }
  }

  private getName(parentType: Type<any>): string {
    switch (parentType) {
      case Login: return 'Login';
      case Register: return 'Register';

      default: throw new Error(`DataService error: Can't find name for type ${parentType}`);
    }
  }
}
