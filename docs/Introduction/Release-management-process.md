In Radical we follow a set of rules to prepare and publish a new release:

* Define the milestone;
* Define an issue for everything that gets touched:
    * the initial issue comment must be as descriptive as possibile;
    * the first 30 lines of the first issue comment will be included by default in the release description;
    * to include a different amount of lines add a HR (--) to the issue first comment;
    * the issue must be labeled at least with one, and only one, of the following labels: Bug, Feature, Improvement;
* Associate the issue to the milestone;
* Use [GitHub Flow](http://scottchacon.com/2011/08/31/github-flow.html) to commit changes;
* Associate a commit with an issue and close it;
* Publish the release associated to the milestone;

This routines allows us to be able to auto-generate [release notes](https://github.com/RadicalFx/radical/blob/develop/ReleaseNotes.md), trying to be compatible with the [Semantic Release Notes](http://www.semanticreleasenotes.org/) using a [release notes compiler](https://github.com/Particular/GitHubReleaseNotes).