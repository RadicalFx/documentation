# Welcome to Radical Framework

{% for page in site.pages %}
* {{ page.url }} - {{ page.title }}, {{site.baseurl}} {{page.path}}
          {% if page.title and page.show_in_header %}
          {% endif %}
        {% endfor %}