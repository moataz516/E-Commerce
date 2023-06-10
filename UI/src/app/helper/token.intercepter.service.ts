import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { Observable } from 'rxjs';
import { AuthenticationService } from '../service/authentication.service';
  
  @Injectable()
  export class TokenIntercepterService implements HttpInterceptor {
    constructor(public auhtService: AuthenticationService) {}
  
    intercept(
      requist: HttpRequest<any>,
      next: HttpHandler
    ): Observable<HttpEvent<any>> {
      if (this.auhtService.isLoggedIn()) {
        const token = localStorage.getItem('jwt');
        requist = requist.clone({
          setHeaders:{
             Authorization: `Bearer ${token}`
          },
        });
      } 
      return next.handle(requist);
    }
  }