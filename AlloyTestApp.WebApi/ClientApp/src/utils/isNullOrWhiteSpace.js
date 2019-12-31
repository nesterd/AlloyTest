export default function isNullOrWhiteSpace(str) {
    return str == null || str.match(/^ *$/) != null;
}