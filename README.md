# Radical Documentation

based on [Jekyll](http://jekyllrb.com) templates.

* do not uses static pages, use `_posts` instead;
* name posts according to the Jekyll convention `year-month-day-slug`
* place posts in subfolder if desired;
* to make links work internally use the `Jekyll liquid` syntax:  `{% post_url post-name %}` where `post_name` is the name of the markdown file with the subdirectory if used:
    * a post saved in `_posts/sample/2010-01-01-my-article-title.md`;
    * will be linked as `{% post_url sample/2010-01-01-my-article-title %}`, without the extension;
    * Jekyll will take care of generating the correct URL;
    * since we live in `vdir` post urls must be prefixed with the `baseurl`: `{{site.baseurl}}`, so the full syntax for internal links is: `[link text]({{site.baseurl}}{% post_url sample/2010-01-01-my-article-title %})`
 * use Jekyll `categories` to create paths, a post categorized as `[desktop, presentation]` will be available as `/desktop/presentation/slug-post-file-name` (without the date, Jekyll will remove it);

### Sample Front Matter header

```
---
layout: article
title: "Quick Start"
categories: [desktop, presentation]
---
```

a `permalink` property can be added to customize the route generation overriding the default behavior based on `categories`.

The ToC will be auto-generated base on `posts` grouped by `category`.