import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError } from 'rxjs/operators';
import { NavigationExtras, Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private _snackBar: MatSnackBar) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              } else if (typeof (error.error) === 'object') {
                this._snackBar.open(error.statusText, null, {
                  duration: 2000
                });
              } else {
                this._snackBar.open(`${error.error}, ${error.status}`, null, {
                  duration: 2000
                });
              }
              break;
            case 401:
              if (error.error.title) {
                this._snackBar.open('Invalid password.', null, {
                  duration: 2000
                });
              } else {
                this._snackBar.open(`${error.error}`, null, {
                  duration: 2000
                });
              }
              break;
            case 404:
              // TODO: /not-found
              this.router.navigateByUrl('/');
              break;
            case 500:
              const navigationExtras: NavigationExtras = { state: { error: error.error } };
              // TODO: /server-error
              this.router.navigateByUrl('/', navigationExtras);
              break;
            default:
              this._snackBar.open('Something unexpected went wrong', null, {
                duration: 2000
              });
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
