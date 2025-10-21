using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionBidPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTennesseeBidFormFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalProvisions",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommencementDays",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommencementOther",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommencementType",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompletionDays",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompletionOther",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompletionType",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorAddress",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorCity",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorLicense",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorName",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorSignaturesJson",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorState",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorZip",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalContractPrice",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FinalPaymentDays",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LenderAddress",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LenderCity",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LenderName",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LenderState",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LenderZip",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OtherContractDocs",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddress",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerCity",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerSignaturesJson",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerState",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerZip",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgressRetentionDays",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgressRetentionPercent",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectAddress",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCity",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectDescription",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectNumber",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectState",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectZip",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProposalDate",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TerminationDate",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WarrantyYears",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkInvolved",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalProvisions",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CommencementDays",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CommencementOther",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CommencementType",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CompletionDays",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CompletionOther",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "CompletionType",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorAddress",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorCity",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorLicense",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorName",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorSignaturesJson",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorState",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ContractorZip",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "FinalContractPrice",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "FinalPaymentDays",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "LenderAddress",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "LenderCity",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "LenderName",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "LenderState",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "LenderZip",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OtherContractDocs",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerAddress",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerCity",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerSignaturesJson",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerState",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "OwnerZip",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProgressRetentionDays",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProgressRetentionPercent",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectAddress",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectCity",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectDescription",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectNumber",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectState",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProjectZip",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ProposalDate",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "TerminationDate",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "WarrantyYears",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "WorkInvolved",
                table: "Bids");
        }
    }
}
