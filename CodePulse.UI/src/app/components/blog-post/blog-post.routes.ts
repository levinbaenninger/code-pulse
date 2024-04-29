import { Routes } from '@angular/router';
import { BlogPostFormComponent } from './blog-post-form/blog-post-form.component';
import { BlogPostListComponent } from './blog-post-list/blog-post-list.component';

export const blogPostRoutes: Routes = [
  { path: '', component: BlogPostListComponent },
  { path: 'new', component: BlogPostFormComponent },
  { path: '**', redirectTo: '/error/404' },
];
