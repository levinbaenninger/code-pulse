import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPost } from '../../../models/blog-post.model';
import { BlogPostService } from '../../../services/blog-post.service';
import { MarkdownComponent } from 'ngx-markdown';

@Component({
  selector: 'app-blog-post-details',
  standalone: true,
  imports: [CommonModule, MarkdownComponent],
  templateUrl: './blog-post-details.component.html',
  styleUrl: './blog-post-details.component.scss',
})
export class BlogPostDetailsComponent implements OnInit, OnDestroy {
  url: string | null = null;
  blogPost?: BlogPost;
  private urlHandleSubscription?: Subscription;
  private blogPostSubscription?: Subscription;

  constructor(
    private activatedRoute: ActivatedRoute,
    private blogPostService: BlogPostService
  ) {}

  ngOnInit(): void {
    this.urlHandleSubscription = this.activatedRoute.params.subscribe(
      (params) => {
        this.url = params['url'];
      }
    );

    if (this.url) {
      this.blogPostSubscription = this.blogPostService
        .getBlogPostByUrl(this.url)
        .subscribe((blogPost) => {
          this.blogPost = blogPost;
        });
    }
  }

  ngOnDestroy(): void {
    this.urlHandleSubscription?.unsubscribe();
    this.blogPostSubscription?.unsubscribe();
  }
}
