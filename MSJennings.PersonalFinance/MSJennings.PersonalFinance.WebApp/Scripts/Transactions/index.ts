export const onLoad = (): void => {
    addEventHandlers()
    initSortIcon()
}

const addEventHandlers = (): void => {
    $('#filterForm').off('submit.filterForm').on('submit.filterForm', submitFilterForm)
    $('#clearButton').off('click.clearButton').on('click.clearButton', clearButtonClick)
    $('#transactionsTable th[data-sortname]').off('click.sortableHeader').on('click.sortableHeader', sortableHeaderClick)
    $('#transactionsTable').off('click.deleteButton', '.deleteButton').on('click.deleteButton', '.deleteButton', deleteButtonClick)
}

const initSortIcon = (): void => {
    const sortName = $('#TransactionsFilter_SortName').val().toString()
    const sortDirection = $('#TransactionsFilter_SortDescending').val().toString().toLocaleLowerCase() === 'true' ? 'down' : 'up'
    const sortIcon = `<i class="fa fa-sort-${sortDirection}"></i>`

    $(`#transactionsTable th[data-sortname=${sortName}]`).append(sortIcon)
}

const submitFilterForm = (): void => {
    $.blockUI()
}

const clearButtonClick = (ev: Event): void => {
    $(ev.target).closest('form').find(':input').val('')
}

const sortableHeaderClick = (ev: Event): void => {
    const sortName: string = $(ev.target).data('sortname')
    $('#TransactionsFilter_SortName').val(sortName)
    $('#filterForm').submit()
}

const deleteButtonClick = (ev: Event): void => {
    bootbox.confirm('Are you sure you want to delete this item?', (result: boolean) => {
        if (result) {
            $.blockUI()
            $(ev.target).closest('form').submit()
        }
    })
}
