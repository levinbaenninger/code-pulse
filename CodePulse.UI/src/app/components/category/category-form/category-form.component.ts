import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './category-form.component.html',
  styleUrl: './category-form.component.scss',
})
export class CategoryFormComponent implements OnInit, OnDestroy {
  categoryForm!: FormGroup;
  private idSubscription?: Subscription;
  private createSubscription?: Subscription;
  private deleteSubscription?: Subscription;
  private updateSubscription?: Subscription;
  id?: string;

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.idSubscription = this.activatedRoute.params.subscribe((params) => {
      this.id = params['id'];

      if (this.id) {
        this.categoryService.getCategory(this.id).subscribe((category) => {
          this.categoryForm.setValue({
            name: category.name,
            urlHandle: category.urlHandle,
          });
        });
      }
    });

    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      urlHandle: ['', Validators.required],
    });
  }

  ngOnDestroy(): void {
    this.idSubscription?.unsubscribe();
    this.createSubscription?.unsubscribe();
    this.updateSubscription?.unsubscribe();
    this.deleteSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    if (this.categoryForm.invalid) {
      return;
    }

    // Check if the category is being updated or created
    if (this.id) {
      this.updateSubscription = this.categoryService
        .updateCategory(this.id, this.categoryForm.value)
        .subscribe(() => this.router.navigate(['/admin/categories']));
      return;
    }

    this.createSubscription = this.categoryService
      .createCategory(this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories']));
  }

  onReset(): void {
    this.categoryForm.reset();
  }

  onDelete(): void {
    if (!this.id) {
      return;
    }

    this.deleteSubscription = this.categoryService
      .deleteCategory(this.id)
      .subscribe(() => this.router.navigate(['/admin/categories']));
  }
}
