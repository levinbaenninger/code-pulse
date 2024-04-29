import { Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { UnknownErrorComponent } from './unknown-error/unknown-error.component';

export const errorRoutes: Routes = [
  { path: '', component: UnknownErrorComponent },
  { path: '404', component: NotFoundComponent },
  { path: '500', component: ServerErrorComponent },
  { path: '**', redirectTo: '404' },
];
