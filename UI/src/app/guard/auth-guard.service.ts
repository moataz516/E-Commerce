import { Injectable } from '@angular/core';
import {  ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate  {

  constructor(private jwtHelper:JwtHelperService, private router: Router) { }

  canActivate():boolean | Observable<boolean> | Promise<boolean>{
    const token = localStorage.getItem('jwt');
    if(token && !this.jwtHelper.isTokenExpired(token) && token !== null){
      return true
    }
    this.router.navigate(["/login"])
    return false
  }

}
