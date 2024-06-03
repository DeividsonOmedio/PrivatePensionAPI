import { Injectable } from '@angular/core';
import { IUser } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginApiService {

  constructor(private http: HttpClient) { }
API_URL = 'https://localhost:7109/api/Auth/login';

login(user: IUser): Observable<any> {
  return this.http.post<any>(this.API_URL, user)
    .pipe(
      catchError(this.handleError)
    );
}

private handleError(error: HttpErrorResponse): Observable<never> {
  // Pode-se adicionar um tratamento de erro mais específico aqui
  let errorMessage = 'An unknown error occurred!';
  if (error.error instanceof ErrorEvent) {
    // Erro no lado do cliente
    errorMessage = `Client-side error: ${error.error.message}`;
  } else {
    // Erro no lado do servidor
    errorMessage = `Server-side error: ${error.status} - ${error.message}`;
  }
  console.error(errorMessage);
  return throwError(errorMessage);
}
}