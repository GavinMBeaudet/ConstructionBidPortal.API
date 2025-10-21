using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionBidPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBidAmountAndTimelineInDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidAmount",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "TimelineInDays",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BidAmount",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TimelineInDays",
                table: "Bids",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
