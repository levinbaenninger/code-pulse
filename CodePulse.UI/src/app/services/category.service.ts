import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../models/category.model';
import { ErrorHandleService } from './error-handle.service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private url = 'Categories';

  constructor(
    private http: HttpClient,
    private errorHandleService: ErrorHandleService
  ) {}

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${environment.apiUrl}/${this.url}`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.errorHandleService.handleError(error);
        return throwError(() => error);
      })
    );
  }

  getCategory(id: string): Observable<Category> {
    return this.http
      .get<Category>(`${environment.apiUrl}/${this.url}/${id}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  createCategory(category: Category): Observable<void> {
    return this.http
      .post<void>(`${environment.apiUrl}/${this.url}`, category)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  updateCategory(id: string, category: Category): Observable<void> {
    return this.http
      .put<void>(`${environment.apiUrl}/${this.url}/${id}`, category)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  deleteCategory(id: string): Observable<void> {
    return this.http
      .delete<void>(`${environment.apiUrl}/${this.url}/${id}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }
}
