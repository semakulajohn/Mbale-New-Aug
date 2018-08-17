Extended directive to also delete branch, this functionality was not in place

tree.remove_branch = function (parent, branch) {
    if (parent != null) {
        var index = parent.children.indexOf(branch)
        parent.children.splice(index, 1);
        parent.expanded = true;
    } else {
        //scope.treeData.push(new_branch);
    }
    return null;
};