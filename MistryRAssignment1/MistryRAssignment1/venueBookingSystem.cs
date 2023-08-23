
namespace MistryRAssignment1
{
    public partial class venueBookingSystem : Form
    {
        //Declare Class level variables

        private int capacity = 0;

        //Total number of available seats
        int totalSeats = 12;

        // Array representing row labels
        private string[] row = { "A", "B", "C" };

        // Array representing column labels
        private string[] column = { "1", "2", "3", "4" };

        //Flag to indicate if customer name is entered
        private bool title = true;

        // Flag to indicate if seats are selected
        private bool selectedSeats = true;

        // Array to store waiting list of customers
        private string[] waitingSeats = new string[10];

        // Define color for available seats
        Color green = Color.Green;

        // Counter for the number of booked seats
        private int number = 0;

        // Array to store booked seats
        private string[,] engagedSeats = new string[3, 4];

        // Flag to indicate if there are empty seats
        private bool emptySeats = false;

        // Dictionary to store seat buttons
        private Dictionary<string, Button> seatButtons;

        // ToolTip control for seat buttons
        private ToolTip seatToolTip;

        public venueBookingSystem()
        {
            InitializeComponent();

            // Initialize the dictionary of seat buttons
            InitializeSeatButtons();

            // Initialize the ToolTip control
            seatToolTip = new ToolTip();

            // Attach ToolTips to each seat button
            AttachToolTipsToButtons();

        }

        // Initialize the seatButtons dictionary with seat labels as keys and corresponding button controls
        private void InitializeSeatButtons()
        {
            seatButtons = new Dictionary<string, Button>()
{
                { "A1", btnA1 },
                { "A2", btnA2 },
                { "A3", btnA3 },
                { "A4", btnA4 },
                { "B1", btnB1 },
                { "B2", btnB2 },
                { "B3", btnB3 },
                { "B4", btnB4 },
                { "C1", btnC1 },
                { "C2", btnC2 },
                { "C3", btnC3 },
                { "C4", btnC4 }
                };
        }

        // Attach MouseHover event to each seat button to display a tooltip
        private void AttachToolTipsToButtons()
        {
            foreach (var button in seatButtons.Values)
            {
                button.MouseHover += btnA1_MouseHover;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnFillAllSeats_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // Event handler for Click button (btnBook) for book the selected seat.
        private void btnBook_Click_1(object sender, EventArgs e)
        {
            lblCustomer.Text = "";

            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                lblCustomer.Text += "Please Enter Your Name \n";
                return; // Exit the method early if the name is not provided
            }

            if (listBoxRows.SelectedIndex == -1 || listBoxColumns.SelectedIndex == -1)
            {
                lblCustomer.Text += "Please select a seat \n";
                return; // Exit the method early if no seat is selected
            }

            int rowIndex = listBoxRows.SelectedIndex;
            int columnIndex = listBoxColumns.SelectedIndex;

            if (string.IsNullOrEmpty(engagedSeats[rowIndex, columnIndex]))
            {
                engagedSeats[rowIndex, columnIndex] = txtCustomerName.Text;

                string seatLabel = row[rowIndex] + column[columnIndex];
                Button selectedButton = seatButtons[seatLabel];
                selectedButton.BackColor = Color.Red;

                lblCustomer.Text = $"The seat {seatLabel} is booked for customer {txtCustomerName.Text} successfully.";

                txtCustomerName.Text = "";
                listBoxRows.SelectedIndex = -1;
                listBoxColumns.SelectedIndex = -1;

                number++;
                totalSeats--;

                lblAvailabilityCount.Text = totalSeats.ToString();
            }
            else
            {
                // Seat is not available
                string bookedCustomer = engagedSeats[rowIndex, columnIndex];
                lblCustomer.Text = $"Seat {row[rowIndex]}{column[columnIndex]} is not available. Booked by {bookedCustomer}.";
            }

        }

        //Assign seat customers from waiting list
        private void AssignSeatFromWaitingList()
        {
            if (capacity > 0 && !emptySeats)
            {
                // Get the first customer who is waiting in the waiting list
                string firstPerson = waitingSeats[0];

                // To find the first available seat 
                bool foundSeat = false;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (String.IsNullOrEmpty(engagedSeats[i, j]))
                        {
                            //To assign the available seat to the first customer from waiting list
                            engagedSeats[i, j] = firstPerson;

                            //assigned the seat to customer and changes color to the red which shows that seat is booked
                            string seatLabel = row[i] + column[j];
                            Button assignedButton = seatButtons[seatLabel];
                            assignedButton.BackColor = Color.Red;
                            lblCustomer.Text = "The seat " + seatLabel + " is booked for " + firstPerson + " successfully.";

                            // To remove the customer from waiting list who is already assigned to seat
                            for (int k = 0; k < capacity - 1; k++)
                            {
                                waitingSeats[k] = waitingSeats[k + 1];
                            }
                            waitingSeats[capacity - 1] = "";

                            // Update capacity and emptySeats 
                            capacity--;
                            emptySeats = true;

                            foundSeat = true;
                            break;
                        }
                    }
                    if (foundSeat)
                        break;
                }

                // Update the waiting list status
                lblStatus.Text = "Waitlist: " + (capacity + 1).ToString();
            }
        }

        //to cancel the booked seats
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            lblCustomer.Text = "";

            // Check if customer select seat horizontally(row) to cancel the seat
            if (listBoxRows.SelectedIndex == -1)
            {
                lblCustomer.Text += "Please select a seat horizontally \n";
                selectedSeats = false;
            }

            // Check if customer select seat vertically(column) to cancel the seat
            if (listBoxColumns.SelectedIndex == -1)
            {
                lblCustomer.Text += "Please select a seat vertically \n";
                selectedSeats = false;
            }

            if (selectedSeats)
            {
                // Check if the selected seat is booked
                if (!string.IsNullOrEmpty(engagedSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex]))
                {
                    string temp = engagedSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex];
                    string shortTerm = row[listBoxRows.SelectedIndex] + column[listBoxColumns.SelectedIndex];

                    Button selectedButton = seatButtons[shortTerm];
                    selectedButton.BackColor = Color.Green;

                    engagedSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] = "";

                    // Show a message box indicating the cancellation
                    MessageBox.Show(temp + "'s Booking Cancelled. ");

                    totalSeats++;  // Increase totalSeats when a seat is canceled

                    // Update the total count of available seats
                    lblAvailabilityCount.Text = totalSeats.ToString();

                    // Check if there are customers on the waiting list
                    if (capacity > 0)
                    {
                        // Assign the seat to the first customer on the waiting list
                        engagedSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] = waitingSeats[0];
                        lblCustomer.Text = "Seat " + row[listBoxRows.SelectedIndex] + column[listBoxColumns.SelectedIndex] + " is booked for " + waitingSeats[0];

                        // Remove the assigned customer from the waiting list
                        for (int i = 0; i < capacity - 1; i++)
                        {
                            waitingSeats[i] = waitingSeats[i + 1];
                        }

                        waitingSeats[capacity - 1] = "";
                        emptySeats = true;
                        string other = row[listBoxRows.SelectedIndex] + column[listBoxColumns.SelectedIndex];
                        capacity--;

                        // Change the color of the seat to red for the assigned customer
                        Button selectedButtonRed = seatButtons[other];
                        selectedButtonRed.BackColor = Color.Red;

                        // Assign the next seat from the waiting list if available
                        AssignSeatFromWaitingList();
                    }
                }
                else
                {
                    // To print the message when the selected seat is available for booking
                    lblCustomer.Text = "Seat " + row[listBoxRows.SelectedIndex] + column[listBoxColumns.SelectedIndex] + " is available for booking";
                }

                selectedSeats = true;
            }

            // Update the current status.
            lblStatus.Text = "Waitlist: " + capacity.ToString();

        }

        private void venueBookingSystem_Load(object sender, EventArgs e)
        {

        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {

        }

        //Button for add to wait list
        private void btnAddToList_Click(object sender, EventArgs e)
        {
            lblCustomer.Text = "";

            //To check and print the message when seat is available
            if (totalSeats == 12)
            {
                lblCustomer.Text = "Sorry, all seats are already available. Please book a seat instead.";
                return;
            }

            //To check and print the message when seat is available
            if (number < 12)
            {
                lblCustomer.Text += "Seats are avilable for booking, please select seat";
            }
            else
            {
                //To print the customer name
                if (string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    lblCustomer.Text += "Please enter your name";
                    return;
                }

                //Update the current status
                lblStatus.Text = "Wait list: " + (capacity + 1).ToString();

                //To add customer in waiting list and print the message
                if (capacity < 10 && emptySeats)
                {
                    waitingSeats[capacity - 1] = txtCustomerName.Text;
                    lblCustomer.Text = txtCustomerName.Text + ", you are added to waiting list. Please wait for some time.";
                    totalSeats--;  // Decrease totalSeats when a customer is added to the waiting list
                    emptySeats = false;
                }

                //To add customers in waiting list and print the message
                else if (capacity < 10)
                {
                    waitingSeats[capacity] = txtCustomerName.Text;
                    lblCustomer.Text = txtCustomerName.Text + " You are added to waiting list. Please wait for some time.";
                    txtCustomerName.Text = "";
                    listBoxRows.SelectedIndex = -1;
                    listBoxColumns.SelectedIndex = -1;
                    capacity++;
                    totalSeats--;  // Decrease totalSeats when a customer is added to the waiting list
                }

                // If capacity of the waiting list are full then it will print the message and not accept the customer for waiting list
                else
                {
                    lblCustomer.Text = "Sorry for inconvenience , but there is no place availble to join waiting list.";
                }
            }

             lblAvailabilityCount.Text = totalSeats.ToString();  // Update the availability count label

        }

        //Button to select all seats
        private void btnFillAllSeats_Click_1(object sender, EventArgs e)
        {
            // Ask name for select all seats
            if (String.IsNullOrEmpty(txtCustomerName.Text))
            {
                lblCustomer.Text = "Please enter Your name";
                title = false;
            }

            else
            {
                string str;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        engagedSeats[i, j] = txtCustomerName.Text;
                        listBoxRows.SelectedIndex = -1;
                        listBoxColumns.SelectedIndex = -1;
                        number++;
                        str = row[i] + column[j];

                        //Changes color to the red for the all booked seats
                        Button selectedButton = seatButtons[str];
                        selectedButton.BackColor = Color.Red;

                    }
                }
                number = 12;
                totalSeats = totalSeats - number;
                // To update the count of the availabile seats
                lblAvailabilityCount.Text = totalSeats.ToString();
                lblCustomer.Text = "";
                //To print the message with customers name
                lblCustomer.Text = "All Seats are taken by " + txtCustomerName.Text;

            }
        }

        //Button to cancel all selected seats
        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            String str;
            lblCustomer.Text = "";

            //Cange the color of the seats to green once all seats are cancelled
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    engagedSeats[i, j] = "";
                    str = row[i] + column[j];

                    Button selectedButton = seatButtons[str];
                    selectedButton.BackColor = Color.Green;
                }
            }
            number = 12;
            totalSeats = totalSeats + number;
            // To update the count of the availabile seats
            lblAvailabilityCount.Text = totalSeats.ToString();
            lblCustomer.Text = "";
            //To print the message when seats are available for book
            lblCustomer.Text = "All Seats are open to book.";
        }

        private void venueBookingSystem_Load_1(object sender, EventArgs e)
        {
        }

        //To create hovering effect on the all seats(buttons)
        private void btnA1_MouseHover(object sender, EventArgs e)
        {
            // Case the sender object to a button
            Button button = (Button)sender;

            //Variable to store the tooltip tex
            string toolTipText;

            //Check if the buttons background colour is red
            if (button.BackColor == Color.Red)
            {
                // Extract the seat label from the button's name
                string seatLabel = button.Name.Replace("btn", "");

                // Find the corresponding row and column index for the seat label
                int rowIndex = Array.IndexOf(row, seatLabel[0].ToString());
                int columnIndex = Array.IndexOf(column, seatLabel[1].ToString());

                // Get the customer name from the engagedSeats array using the row and column indexes
                string customerName = engagedSeats[rowIndex, columnIndex];

                // Create tooltip text indicating that the seat is booked for a specific customer
                toolTipText = " The seat is booked for customer: " + customerName;
            }
            else
            {
                // To print the message when seat is available for booking
                toolTipText = "This seat is available for book";
            }

            // Show the tooltip with the generated text for the button
            seatToolTip.Show(toolTipText, button);
        }

        private void lblAvailabilityCount_Click(object sender, EventArgs e)
        {

        }

        private void lblTotlaCapacity_Click(object sender, EventArgs e)
        {

        }

        private void btnA1_Click(object sender, EventArgs e)
        {

        }
    }
}
