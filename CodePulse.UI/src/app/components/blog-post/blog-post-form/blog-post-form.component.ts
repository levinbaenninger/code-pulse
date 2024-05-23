import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';
import { Subscription } from 'rxjs';
import { Category } from '../../../models/category.model';
import { BlogPostService } from '../../../services/blog-post.service';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-blog-post-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MarkdownModule],
  templateUrl: './blog-post-form.component.html',
  styleUrl: './blog-post-form.component.scss',
})
export class BlogPostFormComponent implements OnInit, OnDestroy {
  blogPostForm!: FormGroup;
  id?: string;
  categories?: Category[];
  private idSubscription?: Subscription;
  private createSubscription?: Subscription;
  private deleteSubscription?: Subscription;
  private updateSubscription?: Subscription;
  private categoriesSubscription?: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private blogPostService: BlogPostService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.blogPostForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      urlHandle: ['', Validators.required],
      content: ['', Validators.required],
      featuredImageUrl: ['', Validators.required],
      datePublished: ['', Validators.required],
      author: ['', Validators.required],
      isVisible: [true, Validators.required],
      categories: this.formBuilder.array([
        this.formBuilder.control('', Validators.required),
      ]),
    });

    this.categoriesSubscription = this.categoryService
      .getCategories()
      .subscribe((categories) => {
        this.categories = categories;
      });

    this.idSubscription = this.activatedRoute.params.subscribe((params) => {
      this.id = params['id'];

      if (this.id) {
        this.blogPostService.getBlogPostById(this.id).subscribe((blogPost) => {
          this.categoryControls.clear();
          blogPost.categories.forEach((category) => {
            this.addCategory(category.id);
          });

          // Convert date to format for frontend
          blogPost.datePublished = new Date(blogPost.datePublished)
            .toISOString()
            .split('T')[0];

          this.blogPostForm.patchValue({
            title: blogPost.title,
            description: blogPost.description,
            urlHandle: blogPost.urlHandle,
            content: blogPost.content,
            featuredImageUrl: blogPost.featuredImageUrl,
            datePublished: blogPost.datePublished,
            author: blogPost.author,
            isVisible: blogPost.isVisible,
          });
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.idSubscription?.unsubscribe();
    this.createSubscription?.unsubscribe();
    this.deleteSubscription?.unsubscribe();
    this.updateSubscription?.unsubscribe();
    this.categoriesSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    if (this.blogPostForm.invalid) {
      return;
    }

    if (this.id) {
      this.updateSubscription = this.blogPostService
        .updateBlogPost(this.id, this.blogPostForm.value)
        .subscribe(() => this.router.navigate(['/admin/blog-posts']));
    } else {
      this.createSubscription = this.blogPostService
        .createBlogPost(this.blogPostForm.value)
        .subscribe(() => {
          this.router.navigate(['/admin/blog-posts']);
        });
    }
  }

  onReset(): void {
    this.categoryControls.clear();
    this.addCategory();
    this.blogPostForm.reset();
  }

  onDelete(): void {
    if (!this.id) {
      return;
    }

    this.deleteSubscription = this.blogPostService
      .deleteBlogPost(this.id)
      .subscribe(() => {
        this.router.navigate(['/admin/blog-posts']);
      });
  }

  get categoryControls() {
    return this.blogPostForm.get('categories') as FormArray;
  }

  addCategory(categoryId?: string): void {
    this.categoryControls?.push(
      this.formBuilder.control(categoryId || '', Validators.required)
    );
  }

  removeCategory(index: number): void {
    this.categoryControls?.removeAt(index);
  }
}
