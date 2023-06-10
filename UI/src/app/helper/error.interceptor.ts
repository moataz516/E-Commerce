import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { Observable, catchError, retry, throwError } from "rxjs";


@Injectable()

export class ErrorInterceptor implements HttpInterceptor{
    constructor(private toastr:ToastrService){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(retry(1),catchError((error: HttpErrorResponse)=>{
            let errorMessage ='';
            console.log(error)

            if(error.error instanceof ErrorEvent && error.status >= 400 ){
              errorMessage = `Error: ${error.error}`
            }else{
                
              errorMessage = `Error Code: ${error.status}\nMessage: ${error.error}`
            }
            this.toastr.error(errorMessage)
            return throwError(errorMessage)
        }))
    }
}