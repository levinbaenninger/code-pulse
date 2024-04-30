import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { BlogPost } from '../models/blog-post.model';
import { ErrorHandleService } from './error-handle.service';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  private url = 'BlogPosts';

  constructor(
    private http: HttpClient,
    private errorHandleService: ErrorHandleService
  ) {}

  getBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.apiUrl}/${this.url}`).pipe(
      catchError((error: HttpErrorResponse) => {
        this.errorHandleService.handleError(error);
        return throwError(() => error);
      })
    );
  }

  getBlogPost(id: string): Observable<BlogPost> {
    return this.http
      .get<BlogPost>(`${environment.apiUrl}/${this.url}/${id}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  createBlogPost(blogPost: BlogPost): Observable<void> {
    return this.http
      .post<void>(`${environment.apiUrl}/${this.url}`, blogPost)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  updateBlogPost(id: string, blogPost: BlogPost): Observable<void> {
    return this.http
      .put<void>(`${environment.apiUrl}/${this.url}/${id}`, blogPost)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.errorHandleService.handleError(error);
          return throwError(() => error);
        })
      );
  }

  deleteBlogPost(id: string): Observable<void> {
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
