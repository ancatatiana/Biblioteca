using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioteca
{
    public partial class Books : System.Web.UI.Page
    {
        private UserClass loggedUser = new UserClass();

        //public UserClass loggedUser = new UserClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //avem grija ca la fiecare prima incarcare a paginii mesajele sa nu fie vizibile
            pnlError.Visible = false;
            pnlSuccess.Visible = false;
            lblError.Visible = false;
            lblSuccess.Visible = false;
            txtNameOfBook.Enabled = true;
            //verifcam sa vedem daca avem user logat in sessiune
            if (Session["user"] != null)
            {
                loggedUser = (UserClass)Session["user"];
                //Label lblUsername = new Label();
                
               // ((Label)Master.FindControl("lblUsername")).Text = loggedUser.username.ToString();

               // ((Label)Master.FindControl("lblRole")).Text = loggedUser.role == 1 ? "User" : "Admin";
                //rol =1 adica use else admin
                //verificam daca este admin sau user
                if (loggedUser.role == 1)
                {
                    txtNameOfBook.Enabled = false;
                    txtAuthor.Enabled = false;
                    ddlYEAR.Enabled = false;
                    ddlISBN.Enabled = false;
                    btnSaveBook.Enabled = false;
                    gridListOfBooks.Columns[6].Visible = false;

                }

            }

            else
            {
                // daca nu avem user il trimitem la pagina de logare
                Session["userNotLoggedIn;"] = "true";
                Response.Redirect("~/Login.aspx");
            }

            string sortColumn = (ViewState["sortColumn"] == null) ? "name_of_book" : ViewState["sortColumn"].ToString();
            string sortOrder = ViewState["sortOrder"] == null ? "asc" : ViewState["sortOrder"].ToString();
            if (ViewState["pageIndex"] != null)
            {
                gridListOfBooks.PageIndex = (int)ViewState["pageIndex"];
            }

            if (!IsPostBack)
            {

                // initializam dropdown listele cu valori
                ddlISBN.Items.Add(new ListItem("-- ISBN --", "0"));
                ddlISBN.Items.Add(new ListItem("978-0-13-601970-0", "978-0-13-601970-0"));
                ddlISBN.Items.Add(new ListItem("978-3-16-148410-4", "978-3-16-148410-4"));

                ddlYEAR.Items.Add(new ListItem("-- Year --", "0"));
                ddlYEAR.Items.Add(new ListItem("1994", "1994"));
                ddlYEAR.Items.Add(new ListItem("2007", "2007"));
                ddlYEAR.Items.Add(new ListItem("1989", "1989"));
                ddlYEAR.Items.Add(new ListItem("2010", "2010"));
                loadRecords(sortColumn, sortOrder);
                //updgridListBooks.Update();
                Panel1.Visible = true;
            }

        }
        // salvaeza carti
        protected void btnSaveBook_Click(object sender, EventArgs e)
        {
            bool eroare = true;
            //cream un obiect de tip carte



            Book book = new Book();
            //populam cartea cu valorile din text boxuri si dropdown listuri
            book.TheNameOfTheBook = txtNameOfBook.Text;
            book.AuthorName = txtAuthor.Text;
            book.TheYearOfTheBook = int.Parse(ddlYEAR.SelectedValue);
            book.ISBN = ddlISBN.SelectedValue;
            try
            {
                // verificam daca exista cartea sa ii facem update
                int bookID = Book.SearchIfBookExist(book, ConnectioString.LocalConnectionString);
                if (bookID != null && bookID != 0)
                {
                    // daca exista cartea ii dam id-ul din baza de date si facem update
                    book.idBook = bookID;
                    Book.UpdateBook(book, ConnectioString.LocalConnectionString);
                    lblSuccess.Text = "Successful Update ";
                    lblSuccess.Visible = true;
                    pnlSuccess.Visible = true;
                    loadRecords("name_of_book", "asc");
                }
                else
                {
                    // nu exista materia si facem insert
                    Book.SaveBook(book, ConnectioString.LocalConnectionString);

                }

                // stergem valorile inregistrate
                txtNameOfBook.Text = "";
                txtAuthor.Text = "";
                ddlISBN.SelectedIndex = 0;
                ddlYEAR.SelectedIndex = 0;
                loadRecords("name_of_book", "asc");
                //gridListOfBooks.Update();
            }
            catch (Exception ex)
            {
                // prindem erorile si le afisam
                lblError.Text = "Failed save! " + ex.Message;
                lblError.Visible = true;
                pnlError.Visible = true;
                eroare = false;
            }
            if (!eroare)
            {

                lblSuccess.Text = "Save with succes";
                lblSuccess.Visible = true;
                pnlSuccess.Visible = true;
            
            }
        }
        #region gridview
        // pentru incarcarea datelor in tabelul din pagina de carti 
        protected void loadRecords(string sortColumn, string sortDirection)
        {
            try
            {
                //cream o lista de carti
                List<Book> ListOfBooks = new List<Book>();
                // selectam lista de carti din baza de datez
                ListOfBooks = Book.SelectBooks(ConnectioString.LocalConnectionString, sortDirection, sortColumn);

                if (ListOfBooks != null)
                {
                    if (ListOfBooks.Count > 0)
                    {
                        // am creat un tabel in care copiem lista de carti
                        DataTable dtListOfBooks = new DataTable();
                        // coloanele tabelului
                        dtListOfBooks.Columns.Add("nr_crt", typeof(int));
                        dtListOfBooks.Columns.Add("id_books", typeof(int));
                        dtListOfBooks.Columns.Add("name_of_book", typeof(string));
                        dtListOfBooks.Columns.Add("author_name", typeof(string));
                        dtListOfBooks.Columns.Add("year", typeof(int));
                        dtListOfBooks.Columns.Add("ISBN", typeof(string));

                        //folosit la sortare
                        if (ViewState["sortColumn"] == null)
                        {
                            ViewState["sortColumn"] = sortColumn;
                        }
                        if (ViewState["sortOrder"] == null)
                        {
                            ViewState["sortOrder"] = sortDirection;
                        }
                        int ct = 1;
                        // pentru fiecare carte din lista de carti copiem randul in tabel
                        foreach (Book book in ListOfBooks)
                        {
                            DataRow row = dtListOfBooks.NewRow();

                            row["nr_crt"] = ct;
                            row["id_book"] = book.idBook;
                            row["name_of_book"] = book.TheNameOfTheBook;
                            row["author_name"] = book.AuthorName;
                            row["year"] = book.TheYearOfTheBook;
                            row["ISBN"] = book.ISBN;

                            dtListOfBooks.Rows.Add(row);
                            ct++;
                        }
                        //populam tabelul din view cu cartiile din baza de date
                        gridListOfBooks.DataSource = dtListOfBooks;
                        gridListOfBooks.DataBind();
                        if (gridListOfBooks.HeaderRow != null)
                            gridListOfBooks.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gridListOfBooks.HeaderRow.Cells[getColumnIndex(sortColumn)].CssClass = "sorted-" + sortDirection;
                        gridListOfBooks.Visible = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        // folosita la sortare
        private int getColumnIndex(string SortExpression)
        {
            int i = 0;
            foreach (DataControlField c in gridListOfBooks.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }

        // folosit la paginile tabelului in cazul in care sunt mai mult de 5 integ
        protected void gridListOfBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridListOfBooks.PageIndex = e.NewPageIndex;

            ViewState["pageIndex"] = gridListOfBooks.PageIndex;
            string sortExpression = ViewState["sortColumn"].ToString();
            string direction = ViewState["sortOrder"].ToString();
            loadRecords(sortExpression, direction);
        }
        // folosita la sortare
        protected void gridListOfBooks_Sorting(object sender, GridViewSortEventArgs e)
        {
            gridListOfBooks.PageIndex = 0;
            string direction = "asc";

            if (ViewState["sortColumn"] != null && ViewState["sortOrder"] != null)
            {
                if (e.SortExpression == ViewState["sortColumn"].ToString() && ViewState["sortOrder"].ToString() == "asc")
                {
                    direction = "desc";
                }
            }

            ViewState["sortColumn"] = e.SortExpression;
            ViewState["sortOrder"] = direction;
            // reincarcam tabelul in functie de coloanele selectate pentru sortare
            loadRecords(e.SortExpression, direction);
        }

        // citimi comenzile din tabel pentru stergere si update
        protected void gridListOfBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id_book = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "updateBook":
                    updateBook(id_book);
                    break;
                case "deleteBook":
                    deletebookCommand(id_book);
                    Response.Redirect("~/Books.aspx");
                    break;
            }
        }

        // stergem cartea
        protected void deletebookCommand(string id_book)
        {
            Book.DeleteBook(ConnectioString.LocalConnectionString, int.Parse(id_book));
        }
        // selectam cartea pt update
        protected void updateBook(string id_book)
        {
            Book book = new Book();
            book = Book.selectBook(ConnectioString.LocalConnectionString, int.Parse(id_book));
            if (book != null)
            {
                txtNameOfBook.Text = book.TheNameOfTheBook;
                txtNameOfBook.Enabled = false;
                txtAuthor.Text = book.AuthorName;
                ddlISBN.SelectedIndex = ddlISBN.Items.IndexOf(ddlISBN.Items.FindByValue(book.ISBN));
                ddlYEAR.SelectedIndex = ddlYEAR.Items.IndexOf(ddlYEAR.Items.FindByValue(book.TheYearOfTheBook.ToString()));
                pnlBook.Update();

            }
            else
            {

                lblError.Text = "The book was not found";
                pnlError.Visible = true;
                lblError.Visible = true;

            }
        }
        #endregion

        protected void gridListOfBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
       
        
        